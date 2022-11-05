namespace Training.Shared
{
    public static class AppConst
    {
        public static class DbSchemas
        {
            public static class Cms
            {
                public static readonly string Name = "Cms";

                public static class Tables
                {
                    public static readonly string Categories = "Categories";
                    public static readonly string Articles = "Articles";
                    //public static readonly string Table1 = "Table1";
                    //public static readonly string Table2 = "Table2";
                    //public static readonly string Table3 = "Table3";
                    // ....
                }
            }

            //public static class SchemaX
            //{
            //    public static readonly string Name = "Cms";
            //    public static class Tables
            //    {
            //        public static readonly string Categories = "Categories";
            //        public static readonly string Table1 = "Table1";
            //        public static readonly string Table2 = "Table2";
            //        public static readonly string Table3 = "Table3";
            //        // ....
            //    }
            //}
        }
    }
}
