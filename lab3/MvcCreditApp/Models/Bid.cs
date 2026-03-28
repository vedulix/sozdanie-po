using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcCreditApp.Models
{
    public class Bid
    {
        // ID заявки
        public virtual int BidId { get; set; }

        // Имя заявителя
        [Required]
        [DisplayName("Имя заявителя")]
        public virtual string Name { get; set; }

        // Название кредита
        [Required]
        [DisplayName("Тип кредита")]
        public virtual string CreditHead { get; set; }

        // Дата подачи заявки
        [DisplayName("Дата подачи")]
        public virtual DateTime bidDate { get; set; }
    }
}
