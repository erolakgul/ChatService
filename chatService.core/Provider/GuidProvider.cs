namespace chatService.core.Provider
{
    public class GuidProvider
    {
        private static GuidProvider _instance;

        private static Guid _id = Guid.NewGuid(); 
        public Guid Id { get { return _id ; } }

        //singleton sınıfına ait sınıfın çalışma zamanında constructordan yararlanarak oluşturulmamamasını sağlar
        private GuidProvider()
        {

        }

        public static GuidProvider GetInstance()
        {
            if (_instance == null)
                _instance = new GuidProvider();

            return _instance;
        }

    }


}