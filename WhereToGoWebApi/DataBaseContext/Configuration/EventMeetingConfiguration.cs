using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhereToGoWebApi.Models;

namespace WhereToGoWebApi.DataBaseContext.Configuration
{
    public class EventMeetingConfiguration : IEntityTypeConfiguration<EventMeeting>
    {
        public void Configure(EntityTypeBuilder<EventMeeting> builder)
        {
            builder.HasOne(x => x.User)
                .WithMany(u => u.EventMeetings)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
