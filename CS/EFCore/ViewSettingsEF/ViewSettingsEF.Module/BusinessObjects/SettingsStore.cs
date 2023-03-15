using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.Persistent.BaseImpl.EF;

namespace ViewSettingsEF.Module.BusinessObjects {
    public class SettingsStore : BaseObject {
        [Browsable(false)]
        public virtual string Xml { get; set; }
        public virtual string Name { get; set; }
        [Browsable(false)]
        public virtual string OwnerId { get; set; }
        public virtual Boolean IsShared { get; set; }
        [Browsable(false)]
        public virtual string ViewId { get; set; }
    }
}
