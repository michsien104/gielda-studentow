using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentExchangeDataAccess.Entity;

namespace gielda_studentow.Service.Interface
{
    public interface IAnnouncementService
    {
        Announcement GetAnnouncementById(long id, string receiverId);
        ICollection<Announcement> GetAllSenderAnnouncements(string senderID);
        ICollection<Announcement> GetAllAnnouncements(string receiverId);
        ICollection<Announcement> SortAnnouncementsByIssueDateNewestFirst(ICollection<Announcement> announcements);
        ICollection<ItemAnnouncement> GetItemAnnouncements(string receiverId);
        void AddBuyItemAnnouncement(BuyItemAnnouncement announcement, string senderId);
        ICollection<BuyItemAnnouncement> GetBuyItemAnnouncementsByReceiverId(string receiverId);
        BuyItemAnnouncement GetBuyItemAnnouncementById(long id);
        void AddSellItemAnnouncement(SellItemAnnouncement announcement, string senderId);
        ICollection<SellItemAnnouncement> GetSellItemAnnouncementsByReceiverId(string receiverId);
        SellItemAnnouncement GetSellItemAnnouncementById(long id);
        ICollection<ItemImage> GetAnnouncementImages(long announcementId);
        void ChangeStatus(long announcementId);
        void AddAnnouncementImage(ItemImage image, long announcementId);
        void AddTakePrivateLessonsAnnouncement(TakePrivateLessonsAnnouncement announcement, string senderId);
        ICollection<TakePrivateLessonsAnnouncement> GetTakePrivateLessonsAnnouncementsByReceiverId(string receiverId);
        TakePrivateLessonsAnnouncement GetTakePrivateLessonsAnnouncementById(long id);
        void AddGivePrivateLessonsAnnouncement(GivePrivateLessonsAnnouncement announcement, string senderId);
        ICollection<GivePrivateLessonsAnnouncement> GetGivePrivateLessonsAnnouncementsByReceiverId(string receiverId);
        GivePrivateLessonsAnnouncement GetGivePrivateLessonsAnnouncementById(long id);
        void AddChangeGroupAnnouncement(ChangeGroupAnnouncement announcement, string senderId);
        ICollection<ChangeGroupAnnouncement> GetChangeGroupAnnouncementsByReceiverId(string receiverId);
        ChangeGroupAnnouncement GetChangeGroupAnnouncementById(long id);
    }
}
