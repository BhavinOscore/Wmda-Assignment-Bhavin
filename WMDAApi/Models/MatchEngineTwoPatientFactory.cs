using System;

namespace WMDAApi.Models
{
    class MatchEngineTwoPatientFactory : MatchEnginePatientFactory
    {
        private Patient _patient;
        public MatchEngineTwoPatientFactory(Patient patient)
        {
            _patient = patient;
        }
        public override MatchEnginePatient GetMatchEnginePatient()
        {
            return new MatchEngineTwoPatient(_patient);
        }
    }
}
