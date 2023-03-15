using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.Xpo;

namespace ViewSettingsSolution.Module.BusinessObjects {
    public class SettingsStore : BaseObject {
        private string xml;
        private string name;
        private string ownerId;
        private string viewId;
        private Boolean isShared;
        private IObjectSpace objectSpace;
        public SettingsStore(Session session)
            : base(session) {
        }
        [Browsable(false)]
        [Size(SizeAttribute.Unlimited)]
        public string Xml {
            get {
                return xml;
            }
            set {
                SetPropertyValue(nameof(Xml), ref xml, value);
            }
        }
        public string Name {
            get {
                return name;
            }
            set {
                SetPropertyValue(nameof(Name), ref name, value);
            }
        }
        [Browsable(false)]
        public string OwnerId {
            get { return ownerId; }
            set { SetPropertyValue(nameof(OwnerId), ref ownerId, value); }
        }
        public Boolean IsShared {
            get {
                return isShared;
            }
            set {
                SetPropertyValue(nameof(IsShared), ref isShared, value);
            }
        }
        [Browsable(false)]
        public string ViewId {
            get { return viewId; }
            set { SetPropertyValue<string>(nameof(ViewId), ref viewId, value); }
        }

       
    }
}
