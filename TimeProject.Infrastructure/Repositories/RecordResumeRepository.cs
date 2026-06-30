using TimeProject.Domain.Entities;
using TimeProject.Domain.Repositories;
using TimeProject.Infrastructure.Database;
using TimeProject.Infrastructure.Utils;

namespace TimeProject.Infrastructure.Repositories;

public class RecordResumeRepository(CustomDbContext dbContext) : IRecordResumeRepository
{
    public RecordResume? CreateOrUpdate(int recordId, bool saveChanges = true)
    {
        var record = dbContext.Records.FirstOrDefault(e => e.RecordId == recordId);
        return record == null ? null : CreateOrUpdate(record, saveChanges);
    }

    public IEnumerable<RecordResume> CreateOrUpdateList(IEnumerable<Record> recordEntities, bool saveChanges = false)
    {
        var list = new List<RecordResume>();

        foreach (var recordEntity in recordEntities)
        {
            var meta = CreateOrUpdate(recordEntity, saveChanges);
            if (meta != null) list.Add(meta);
        }

        dbContext.SaveChanges();

        return list;
    }

    public RecordResume? CreateOrUpdate(Record record, bool saveChanges = true)
    {
        var entity = dbContext.RecordResumes.FirstOrDefault(e => e.RecordId == record.RecordId);

        var periods = dbContext.Periods
            .Where(e => e.RecordId == record.RecordId)
            .OrderBy(e => e.Start)
            .ToList();

        var timeMinutes = dbContext.Minutes
            .Where(e => e.RecordId == record.RecordId)
            .OrderBy(e => e.Date)
            .ToList();

        var timeSpan = periods.TimeSpanFromPeriods().Add(TimeFormatEntitiesUtil.TimeSpanFromMinutes(timeMinutes));
        var formattedTime = TimeFormatEntitiesUtil.StringFromTimeSpan(timeSpan);

        var firstList = new List<DateTimeOffset>();
        var lastList = new List<DateTimeOffset>();

        if (periods.Count > 0)
        {
            firstList.Add(periods[0].Start);
            lastList.Add(periods[^1].Start);
        }

        if (timeMinutes.Count > 0)
        {
            firstList.Add(timeMinutes[0].Date);
            lastList.Add(timeMinutes[^1].Date);
        }

        DateTimeOffset? first = firstList.Count > 0 ? firstList.OrderBy(i => i).First() : null;
        DateTimeOffset? last = lastList.Count > 0 ? lastList.OrderByDescending(i => i).First() : null;

        if (entity == null)
        {
            entity = new RecordResume
            {
                RecordId = record.RecordId,
                Count = periods.Count + timeMinutes.Count,
                Formatted = formattedTime,
                Seconds = timeSpan.TotalSeconds,
                FirstDate = first,
                LastDate = last,
                UserId = record.UserId
            };

            dbContext.Add(entity);
        }
        else
        {
            entity.Count = periods.Count + timeMinutes.Count;
            entity.Seconds = timeSpan.TotalSeconds;
            entity.Formatted = formattedTime;
            entity.FirstDate = first;
            entity.LastDate = last;
        }

        if (saveChanges) dbContext.SaveChanges();

        return entity;
    }
}