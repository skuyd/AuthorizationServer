namespace AuthServer.Domain.Sys
{
    /// <summary>
    /// 用户可访问的客户端
    /// </summary>
    public class MUserClient
    {
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }


        public virtual string ClientId { get; set; }

        public virtual ClientEntity Client { get; set; }
    }
}
