using TimeProject.Application.Interfaces.UseCases.Statistics;
using TimeProject.Infrastructure.ObjectValues;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.Dtos.Statistics;
using TimeProject.Domain.Entities.Enums;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.Database.Entities;
using TimeProject.Infrastructure.ObjectValues.Statistics;
using TimeProject.Infrastructure.Utils;
using TimeProject.Infrastructure.Utils.Interfaces;

namespace TimeProject.Application.UseCases.Statistics;

public class GetRangeDaysStatisticUseCase(
    IStatisticRepository statisticRepository,
    IRecordRepository recordRepository,
    IPeriodCutUtil periodCutUtil,
    IRecordMapDataUtil mapDataUtil
)
    : IGetRangeDaysStatisticUseCase
{
    public ICustomResult<IRangeStatistic> Handle(
        int userId,
        DateTimeOffset? start = null,
        DateTimeOffset? end = null,
        int? recordId = null,
        bool skipRangeProgress = false
    )
    {
        return new CustomResult<IRangeStatistic>().SetData(
            (_handle(userId, start, end, recordId, skipRangeProgress)).Statistic
        );
    }

    public ICustomResult<IRangeStatisticsWithDays> Handle(int userId, DateTimeOffset start, DateTimeOffset end)
    {
        var daysFromRange = new List<DateTimeOffset> { start };
        var daysStatistics = new List<IRangeStatistic>();

        var periods = new List<Period>();
        var minutes = new List<Minute>();
        var sessions = new List<Session>();

        var dayOnRange = start.AddDays(1);

        while (dayOnRange < end)
        {
            daysFromRange.Add(dayOnRange);
            dayOnRange = dayOnRange.AddDays(1);
        }

        daysFromRange.Add(end);

        var daysCount = daysFromRange.Count;
        var activeDaysCount = 0;

        foreach (var day in daysFromRange)
        {
            var result = _handle(userId, day, null, null, true);
            periods.AddRange(result.Periods.OfType<Period>());
            minutes.AddRange(result.Minutes.OfType<Minute>());
            sessions.AddRange(result.Sessions.OfType<Session>());
            daysStatistics.Add((RangeStatistic)result.Statistic);

            if (result.Statistic.TotalInMinutes > 0) activeDaysCount++;
        }

        var rangeStatistics = MakeRangeStatisticDatas(
            start,
            end,
            periods,
            minutes,
            sessions,
            new List<Record>(),
            daysCount,
            activeDaysCount
        ).Statistic;

        rangeStatistics.RecordRangeProgress = MakeRangeProgress(
            GetRecordsByRange(userId, periods, minutes),
            periods,
            minutes
        );


        return new CustomResult<IRangeStatisticsWithDays>().SetData(new RangeStatisticsWithDays
        {
            Total = rangeStatistics,
            Days = daysStatistics.OrderByDescending(e => e.StartDay).ToList()
        });
    }

    private RangeStatisticsData _handle(
        int userId,
        DateTimeOffset? start = null,
        DateTimeOffset? end = null,
        int? recordId = null,
        bool skipRangeProgress = false
    )
    {
        var initDate = start ?? DateTime.Today.ToUniversalTime();
        var endDate = end ?? initDate.AddDays(1).AddMicroseconds(-1);

        var periodsByRange = statisticRepository
            .GetPeriodsByRange(userId, initDate, endDate, recordId);

        var periods = periodCutUtil.Handle(periodsByRange.OfType<Period>(), initDate, endDate).ToList();

        var sessions =
            statisticRepository.GetSessionsByRange(userId, initDate, endDate, recordId).OfType<Session>()
                .Select(e =>
                {
                    e.Periods = periodCutUtil.Handle(e.Periods!, initDate, endDate);
                    return e;
                })
                .ToList();

        var timeMinutes = statisticRepository
            .GetTimeMinutesByRange(userId, initDate, endDate, recordId).OfType<Minute>().ToList();

        var records = new List<Record>();

        if (recordId == null && !skipRangeProgress)
        {
            var trIdList = new List<int>();
            trIdList.AddRange(periods.Where(e => e.RecordId.HasValue).Select(e => e.RecordId!.Value));
            trIdList.AddRange(timeMinutes.Where(e => e.RecordId.HasValue).Select(e => e.RecordId!.Value));
            records.AddRange(recordRepository.FindByIdList(trIdList.Distinct(), userId).OfType<Record>());
        }

        return MakeRangeStatisticDatas(
            initDate,
            endDate,
            periods,
            timeMinutes,
            sessions,
            records
        );
    }

    private RangeStatisticsData MakeRangeStatisticDatas(
        DateTimeOffset start,
        DateTimeOffset end,
        List<Period> periods,
        List<Minute> minutes,
        List<Session> sessions,
        List<Record> records,
        int daysCount = 0,
        int activeDaysCount = 0
    )
    {
        var manualPeriods = periods.Where(e => e.SessionId == null).ToList();

        var timerSessions = sessions.Where(e => e.Type == SessionType.Timer).ToList();
        var pomodoroSessions = sessions.Where(e => e.Type == SessionType.Pomodoro).ToList();
        var breakSessions = sessions.Where(e => e.Type == SessionType.Break).ToList();

        var manualPeriodsTimeSpan = TimeFormatUtil.TimeSpanFromPeriods(manualPeriods);
        var allPeriodsTimeSpan = TimeFormatUtil.TimeSpanFromPeriods(periods);
        var minutesTimeSpan = TimeFormatUtil.TimeSpanFromMinutes(minutes);

        var totalTimeSpan = allPeriodsTimeSpan.Add(minutesTimeSpan);
        var totalManualTimeSpan = manualPeriodsTimeSpan.Add(minutesTimeSpan);
        var totalSessionTimeSpan = TimeFormatUtil.TimeSpanFromSessions(sessions);

        var rangeProgress = MakeRangeProgress(records, periods, minutes);

        var totalDays = (end - start).TotalDays;
        if (totalDays == 0) totalDays = 1;

        var hoursAvarege = activeDaysCount != 0
            ? totalTimeSpan.TotalHours / activeDaysCount
            : totalTimeSpan.TotalHours / totalDays;

        var minutesAvarege = activeDaysCount != 0
            ? totalTimeSpan.TotalMinutes / activeDaysCount
            : totalTimeSpan.TotalMinutes / totalDays;

        var avarageTimeSpan = TimeSpan.FromHours(hoursAvarege);


        return new RangeStatisticsData
        {
            Periods = periods.OfType<IPeriod>().ToList(),
            Minutes = minutes.OfType<IMinute>().ToList(),
            Sessions = sessions.OfType<ISession>().ToList(),
            Statistic = new RangeStatistic
            {
                StartDay = start,
                EndDay = end,

                TotalHours = TimeFormatUtil.StringFromTimeSpan(totalTimeSpan),
                ManualHours = TimeFormatUtil.StringFromTimeSpan(totalManualTimeSpan),
                MinuteHours = TimeFormatUtil.StringFromTimeSpan(minutesTimeSpan),
                ManualPeriodHours = TimeFormatUtil.StringFromPeriods(manualPeriods),

                TotalInHours = totalTimeSpan.TotalHours,
                TotalInMinutes = totalTimeSpan.TotalMinutes,

                AverageInHours = hoursAvarege,
                AverageInMinutes = minutesAvarege,
                AverageHours = TimeFormatUtil.StringFromTimeSpan(avarageTimeSpan),

                DaysCount = daysCount,
                ActiveDaysCount = activeDaysCount,

                TimerHours = TimeFormatUtil.StringFromSessions(timerSessions),
                PomodoroHours = TimeFormatUtil.StringFromSessions(pomodoroSessions),
                BreakHours = TimeFormatUtil.StringFromSessions(breakSessions),

                TotalTimeSpan = totalTimeSpan,
                ManualPeriodsTimeSpan = manualPeriodsTimeSpan,
                MinutesTimeSpan = minutesTimeSpan,
                SessionsTimeSpan = totalSessionTimeSpan,

                TimerCount = timerSessions.Count,
                PomodoroCount = pomodoroSessions.Count,
                BreakCount = breakSessions.Count,
                IsolatedPeriodCount = manualPeriods.Count,
                ManualCount = manualPeriods.Count + minutes.Count,

                PeriodCount = periods.Count,
                SessionCount = sessions.Count,
                MinuteCount = minutes.Count,

                RecordRangeProgress = rangeProgress
            }
        };
    }

    private IList<IRecordRangeProgress> MakeRangeProgress(
        List<Record> records,
        List<Period> allPeriods,
        List<Minute> allMinutes
    )
    {
        var rangeProgressList = new List<IRecordRangeProgress>();

        foreach (var record in records)
        {
            var periods = allPeriods.Where(e => e.RecordId == record.RecordId);

            var minutes = allMinutes.Where(e => e.RecordId == record.RecordId);

            var periodsTimeSpan = TimeFormatUtil.TimeSpanFromPeriods(periods);
            var minutesTimeSpan = TimeFormatUtil.TimeSpanFromMinutes(minutes);
            var totalHoursTimeSpan = periodsTimeSpan.Add(minutesTimeSpan);

            rangeProgressList.Add(new RecordRangeProgress
            {
                Record = mapDataUtil.Handle(record),
                TotalHours = TimeFormatUtil.StringFromTimeSpan(totalHoursTimeSpan),
                TotalTimeSpan = totalHoursTimeSpan
            });
        }

        return rangeProgressList.OrderByDescending(i => i.TotalTimeSpan).ToList();
    }

    private List<Record> GetRecordsByRange(
        int userId,
        IEnumerable<Period> periods,
        IEnumerable<Minute> minutes
    )
    {
        var trIdList = new List<int>();
        trIdList.AddRange(periods.Where(e => e.RecordId.HasValue).Select(e => e.RecordId!.Value));
        trIdList.AddRange(minutes.Where(e => e.RecordId.HasValue).Select(e => e.RecordId!.Value));
        return recordRepository.FindByIdList(trIdList.Distinct().ToList(), userId).OfType<Record>().ToList();
    }
}