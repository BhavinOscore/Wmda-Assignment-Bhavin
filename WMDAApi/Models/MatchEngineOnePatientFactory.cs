namespace WMDAApi.Models
{
    class MatchEngineOnePatientFactory : MatchEnginePatientFactory
    {
        private Patient _patient;
        public MatchEngineOnePatientFactory(Patient patient)
        {
            _patient = patient;
        }
        public override MatchEnginePatient GetMatchEnginePatient()
        {
            return new MatchEngineOnePatient(_patient);
        }
    }
}
