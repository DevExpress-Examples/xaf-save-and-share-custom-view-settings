using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;

namespace ViewSettingsSolution.Module.BusinessObjects {
    [DefaultClassOptions]
    public class Contact : BaseObject {
        private string webPageAddress;
        private string nickName;
        private string spouseName;
        private TitleOfCourtesy titleOfCourtesy;
        public Contact(Session session) :
            base(session) {
        }
        public string WebPageAddress {
            get {
                return webPageAddress;
            }
            set {
                SetPropertyValue("WebPageAddress", ref webPageAddress, value);
            }
        }
        public string NickName {
            get {
                return nickName;
            }
            set {
                SetPropertyValue("NickName", ref nickName, value);
            }
        }
        public string SpouseName {
            get {
                return spouseName;
            }
            set {
                SetPropertyValue("SpouseName", ref spouseName, value);
            }
        }
        public TitleOfCourtesy TitleOfCourtesy {
            get {
                return titleOfCourtesy;
            }
            set {
                SetPropertyValue("TitleOfCourtesy", ref titleOfCourtesy, value);
            }
        }
    }
    public enum TitleOfCourtesy {
        Dr,
        Miss,
        Mr,
        Mrs,
        Ms
    };
}
