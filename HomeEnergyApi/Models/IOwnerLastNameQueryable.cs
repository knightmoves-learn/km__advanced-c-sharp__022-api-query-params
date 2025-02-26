namespace HomeEnergyApi.Models
{
    public interface IOwnerLastNameQueryable<T>
    {
        List<T> FindByLastName(string lastName);
    }
}