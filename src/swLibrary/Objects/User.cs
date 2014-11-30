using TreeGecko.Library.Common.Enums;
using TreeGecko.Library.Common.Objects;
using TreeGecko.Library.Net.Objects;

namespace Sidwatch.Library.Objects
{
    public class User : TGUser
    {
        public UserTypes UserType { get; set; }


        public override TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject tgs = base.GetTGSerializedObject();

            tgs.Add("UserType", UserType);

            return tgs;
        }

        public override void LoadFromTGSerializedObject(TGSerializedObject _tgs)
        {
            base.LoadFromTGSerializedObject(_tgs);

            UserType = (UserTypes)_tgs.GetEnum("UserType", typeof (UserTypes), UserTypes.User);
        }
    }
}
