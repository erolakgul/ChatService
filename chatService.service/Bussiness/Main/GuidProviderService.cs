namespace chatService.service.Bussiness.Main
{
    public class GuidProviderService
    {
        private static GuidProviderService _instance;

        private static Guid _id = Guid.NewGuid(); 
        public Guid Id { get { return _id ; } }

        //singleton sınıfına ait sınıfın çalışma zamanında constructordan yararlanarak oluşturulmamamasını sağlar
        private GuidProviderService()
        {

        }

        public static GuidProviderService GetInstance()
        {
            if (_instance == null)
                _instance = new GuidProviderService();

            return _instance;
        }

    }


}