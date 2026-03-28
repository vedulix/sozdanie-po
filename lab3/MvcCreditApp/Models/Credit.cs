using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcCreditApp.Models
{
    public class Credit
    {
        // ID кредита
        public virtual int CreditId { get; set; }

        // Название
        [Required]
        [DisplayName("Тип кредита")]
        public virtual string Head { get; set; }

        // Период, на который выдается кредит
        [Required]
        [DisplayName("Период кредитования")]
        public virtual int Period { get; set; }

        // Максимальная сумма кредита
        [Required]
        [DisplayName("Максимальная сумма")]
        public virtual int Sum { get; set; }

        // Процентная ставка
        [Required]
        [DisplayName("Процентная ставка %")]
        public virtual int Procent { get; set; }
    }
}
