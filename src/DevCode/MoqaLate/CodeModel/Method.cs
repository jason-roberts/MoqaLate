namespace MoqaLate.CodeModel
{
    public class Method
    {
        public Method()
        {
            Parameters = new MethodParameterList();
        }
        public string ReturnType { get; set; }

        public string Name { get; set; }

        public MethodParameterList Parameters { get; set; }
    }
}
