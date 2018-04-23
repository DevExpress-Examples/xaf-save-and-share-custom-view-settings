using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.Xpo;

namespace ViewSettingsSolution.Module.BusinessObjects {
    public class SettingsStore : BaseObject, IObjectSpaceLink {
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
                SetPropertyValue("XML", ref xml, value);
            }
        }
        public string Name {
            get {
                return name;
            }
            set {
                SetPropertyValue("Name", ref name, value);
            }
        }
        [Browsable(false)]
        public string OwnerId {
            get { return ownerId; }
            set { SetPropertyValue<string>("OwnerId", ref ownerId, value); }
        }
        public Boolean IsShared {
            get {
                return isShared;
            }
            set {
                SetPropertyValue("IsShared", ref isShared, value);
            }
        }
        [Browsable(false)]
        public string ViewId {
            get { return viewId; }
            set { SetPropertyValue<string>("ViewId", ref viewId, value); }
        }

        IObjectSpace IObjectSpaceLink.ObjectSpace {
            get {
                return objectSpace;
            }

            set {
                objectSpace = value;
            }
        }
    }
}
