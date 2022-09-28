namespace chatService.core.Provider
{
    public class GuidProvider
    {
        private static GuidProvider _instance;

        private static Guid _id = Guid.NewGuid(); 
        public Guid Id { get { return _id ; } }

        private GuidProvider()
        {

        }

        /// <summary>
        /// create instance for class
        /// </summary>
        /// <returns></returns>
        public static GuidProvider GetInstance()
        {
            if (_instance == null)
                _instance = new GuidProvider();

            return _instance;
        }

    }


}