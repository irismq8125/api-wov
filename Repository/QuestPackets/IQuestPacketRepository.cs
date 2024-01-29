namespace api_wov.Repository.QuestPackets
{
    public interface IQuestPacketRepository<TEntity, TEntityOutput, TKey>
    {
        Task<TEntityOutput> GetPacketByQuestId(TKey id);
        void AddAll(string questid);
        void AddQuestPacket(string questid, string packetId);
    }
}
