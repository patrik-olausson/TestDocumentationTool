using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;

namespace TestDocumentationTool.Api
{
    public class ProjectInfo
    {
        private Dictionary<string, UnitUnderTest> _unitsUnderTest;
        public string Name { get; }

        public IEnumerable<UnitUnderTest> UnitsUnderTest => _unitsUnderTest.Values;
        public bool HasInformation => _unitsUnderTest.Values.Any(x => x.HasInformation);

        public ProjectInfo(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));

            Name = name;
            _unitsUnderTest = new Dictionary<string, UnitUnderTest>(StringComparer.InvariantCultureIgnoreCase);
        }


        public UnitUnderTest GetOrCreateUnitUnderTest(TypeDefinition publicClass)
        {
            UnitUnderTest unitUnderTest;
            if (_unitsUnderTest.TryGetValue(publicClass.Namespace, out unitUnderTest))
            {
                return unitUnderTest;
            }
            
            unitUnderTest = new UnitUnderTest(publicClass.Namespace, publicClass.Name);
            AddUnitUnderTest(unitUnderTest);

            return unitUnderTest;
        }

        public void Compress()
        {
            foreach (var unitUnderTest in _unitsUnderTest.Values)
            {
                unitUnderTest.Compress();
            }
            _unitsUnderTest = _unitsUnderTest.Where(x => x.Value.HasInformation).ToDictionary(x => x.Key, x => x.Value);
        }

        internal void AddUnitUnderTest(UnitUnderTest unitUnderTest)
        {
            _unitsUnderTest.Add(unitUnderTest.Namespace, unitUnderTest);
        }
    }
}