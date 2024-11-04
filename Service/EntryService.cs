﻿using M223PunchclockDotnet.Model;
using Microsoft.EntityFrameworkCore;

namespace M223PunchclockDotnet.Service
{
    public class EntryService(DatabaseContext databaseContext)
    {
        public Task<List<Entry>> FindAll()
        {
            return databaseContext.Entries.ToListAsync();
        }

        public async Task<Entry> AddEntry(Entry entry)
        {
            databaseContext.Entries.Add(entry);
            await databaseContext.SaveChangesAsync();

            return entry;
        }

        public async Task DeleteEntry(int entryId)
        {
            var entry = await databaseContext.FindAsync<Entry>(entryId);
            if (entry is null)
            {
                throw new NullReferenceException($"No entry with id {entryId} found.");
            }
            
            databaseContext.Entries.Remove(entry);
            await databaseContext.SaveChangesAsync();
        }
    }
}
