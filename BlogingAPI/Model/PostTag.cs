namespace BlogingAPI.Model
{
        public class PostTag
        {
            public int PostId { get; set; }
            public BlogPost BlogPost { get; set; }

            public int TagId { get; set; }
            public Tag Tag { get; set; }
        }
    

}
