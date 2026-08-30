using System.ComponentModel.DataAnnotations;

namespace Hotels.Domain.Entities.BaseEntities
{
    public enum ActionType
    {
        Created = 1,
        Updated = 2,
        Deleted = 3,
    }
}
