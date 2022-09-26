using chatService.core.Services.Basis;
using chatService.core.UOW;

namespace chatService.service.Bussiness.Basis
{
    public class GuidGenerator
    {
        private static GuidGenerator _instance;

        private static Guid _id = Guid.NewGuid(); //nesnenin tek olduğunun ispati
        public Guid Id { get { return _id ; } }

        //singleton sınıfına ait sınıfın çalışma zamanında constructordan yararlanarak oluşturulmamamasını sağlar
        private GuidGenerator()
        {

        }

        public static GuidGenerator GetInstance()
        {
            if (_instance == null)
                _instance = new GuidGenerator();

            return _instance;
        }

    }
}