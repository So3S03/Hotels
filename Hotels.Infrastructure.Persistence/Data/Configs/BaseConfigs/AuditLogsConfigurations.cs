using Hotels.Domain.Entities.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotels.Infrastructure.Persistence.Data.Configs.BaseConfigs
{
    public class AuditLogsConfigurations : BaseEntityConfigurations<AuditLog, string>
    {
        public override void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            base.Configure(builder);
            builder.HasIndex(A => new { A.EntityId, A.EntityName }); //Composite Key For Fast Searching
            builder.Property(A => A.EntityName).IsRequired();
            builder.Property(A => A.EntityId).IsRequired();
            builder.Property(A => A.UserId).IsRequired();
            builder.Property(A => A.UserName).HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(A => A.ActionDate).HasColumnType("datetime2").IsRequired();
            builder.Property(A => A.ActionType).HasConversion(
                (at) => at.ToString(),
                (at) => (ActionType)Enum.Parse(typeof(ActionType), at)
                ).IsRequired();
        }
    }
}
