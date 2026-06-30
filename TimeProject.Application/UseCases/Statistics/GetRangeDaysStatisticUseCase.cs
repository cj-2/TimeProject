using TimeProject.Application.Dtos.Statistics;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.UseCases.Statistics;
using TimeProject.Application.Interfaces.Utils;
using TimeProject.Application.Shared;
using TimeProject.Application.Utils;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.Entities.Enums;
using TimeProject.Infrastructure.Utils;

namespace TimeProject.Application.UseCases.Statistics;

public class GetRangeDaysStatisticUseCase(
    IStatisticRepository statisticRepository,
    IRecordRepository recordRepository,
    IPeriodCutUtil periodCutUtil,
    IRecordMapDataUtil mapDataUtil
)
    : IGetRangeDaysStatisticUseCase
{
    public ICustomResult<RangeStatisticOutDto> Handle(int userId,
        DateTimeOffset? start = null,
        DateTimeOffset? end = null,
        int? recordId = null,
        bool skipRangeProgress = false)
    {
        return new CustomResult<RangeStatisticOutDto>().SetData(
            (_handle(userId, start, end, recordId, skipRangeProgress)).StatisticOutDto
        );
    }

    public ICustomResult<RangeStatisticsWithDaysOutDto> Handle(int userId, DateTimeOffset start, DateTimeOffset end)
    {
        var daysFromRange = new List<DateTimeOffset> { start };
        var daysStatistics = new List<RangeStatisticOutDto>();

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
            daysStatistics.Add(result.StatisticOutDto);

            if (result.StatisticOutDto.TotalInMinutes > 0) activeDaysCount++;
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
        ).StatisticOutDto;

        rangeStatistics.RecordRangeProgress = MakeRangeProgress(
            GetRecordsByRange(userId, periods, minutes),
            periods,
            minutes
        );


        return new CustomResult<RangeStatisticsWithDaysOutDto>().SetData(new RangeStatisticsWithDaysOutDto
        {
            Total = rangeStatistics,
            Days = daysStatistics.OrderByDescending(e => e.StartDay).ToList()
        });
    }

    private RangeStatistics _handle(
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

    private RangeStatistics MakeRangeStatisticDatas(
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

        var manualPeriodsTimeSpan = manualPeriods.TimeSpanFromPeriods();
        var allPeriodsTimeSpan = periods.TimeSpanFromPeriods();
        var minutesTimeSpan = minutes.TimeSpanFromMinutes();

        var totalTimeSpan = allPeriodsTimeSpan.Add(minutesTimeSpan);
        var totalManualTimeSpan = manualPeriodsTimeSpan.Add(minutesTimeSpan);
        var totalSessionTimeSpan = sessions.TimeSpanFromSessions();

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


        return new RangeStatistics
        {
            Periods = periods.ToList(),
            Minutes = minutes.ToList(),
            Sessions = sessions.ToList(),
            StatisticOutDto = new RangeStatisticOutDto
            {
                StartDay = start,
                EndDay = end,

                TotalHours = totalTimeSpan.StringFromTimeSpan(),
                ManualHours = totalManualTimeSpan.StringFromTimeSpan(),
                MinuteHours = minutesTimeSpan.StringFromTimeSpan(),
                ManualPeriodHours = manualPeriods.StringFromPeriods(),

                TotalInHours = totalTimeSpan.TotalHours,
                TotalInMinutes = totalTimeSpan.TotalMinutes,

                AverageInHours = hoursAvarege,
                AverageInMinutes = minutesAvarege,
                AverageHours = avarageTimeSpan.StringFromTimeSpan(),

                DaysCount = daysCount,
                ActiveDaysCount = activeDaysCount,

                TimerHours = timerSessions.StringFromSessions(),
                PomodoroHours = pomodoroSessions.StringFromSessions(),
                BreakHours = breakSessions.StringFromSessions(),

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

    private IList<RecordRangeProgress> MakeRangeProgress(
        List<Record> records,
        List<Period> allPeriods,
        List<Minute> allMinutes
    )
    {
        var rangeProgressList = new List<RecordRangeProgress>();

        foreach (var record in records)
        {
            var periods = allPeriods.Where(e => e.RecordId == record.RecordId);
            var minutes = allMinutes.Where(e => e.RecordId == record.RecordId);

            var periodsTimeSpan = periods.TimeSpanFromPeriods();
            var minutesTimeSpan = minutes.TimeSpanFromMinutes();
            var totalHoursTimeSpan = periodsTimeSpan.Add(minutesTimeSpan);

            rangeProgressList.Add(new RecordRangeProgress
            {
                Record = mapDataUtil.Handle(record),
                TotalHours = totalHoursTimeSpan.StringFromTimeSpan(),
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