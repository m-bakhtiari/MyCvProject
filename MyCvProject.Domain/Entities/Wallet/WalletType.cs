using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCvProject.Domain.Entities.Wallet
{
    public class WalletType
    {
        #region Constructor

        public WalletType()
        {

        }

        #endregion

        /// <summary>
        /// آیدی نوع تراکنش
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeId { get; set; }

        /// <summary>
        /// عنوان
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string TypeTitle { get; set; }

        #region Relations

        /// <summary>
        /// لیست کیف پول
        /// </summary>
        public virtual List<Wallet> Wallets { get; set; }

        #endregion  
    }
}
