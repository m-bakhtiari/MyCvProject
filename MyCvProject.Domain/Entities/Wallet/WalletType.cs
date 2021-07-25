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

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeId { get; set; }

        [Required]
        [MaxLength(150)]
        public string TypeTitle { get; set; }

        #region Relations
        public virtual List<Wallet> Wallets { get; set; }

        #endregion  
    }
}
