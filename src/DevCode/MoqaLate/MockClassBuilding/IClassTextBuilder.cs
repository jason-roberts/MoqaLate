using MoqaLate.CodeModel;

namespace MoqaLate.MockClassBuilding
{
    public interface IClassTextBuilder
    {
        string Create(ClassSpecification spec);
    }
}