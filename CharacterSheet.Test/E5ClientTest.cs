using CharacterSheet.Data.E5;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace CharacterSheet.Test
{
    [TestClass]
    public class E5ClientTest
    {
        private E5Client e5Client;

        [TestInitialize]
        public void Initialize()
        {
            e5Client = new E5Client();
        }

        [TestCleanup]
        public void Cleanup()
        {
        }

        [TestMethod]
        public async Task Client_Should_Return_Equipments()
        {
            var equipmentList = await e5Client.GetEquipmentsAsync();
            Assert.IsNotNull(equipmentList);
            Assert.IsTrue(equipmentList.Count > 0);
            Assert.IsTrue(equipmentList.Results.Length > 0);
            Assert.IsNotNull(equipmentList.Results[0].Index);
            Assert.IsNotNull(equipmentList.Results[0].Name);
        }

        [TestMethod]
        public async Task Client_Should_Return_Equipment_Club()
        {
            var equipmentClub = (Weapon)await e5Client.GetEquipmentDetailsAsync("club");
            Assert.IsNotNull(equipmentClub);
            Assert.IsNotNull(equipmentClub.Index);
            Assert.IsNotNull(equipmentClub.Name);
            Assert.IsNotNull(equipmentClub.Weight);
            Assert.IsNotNull(equipmentClub.Cost.Quantity);
            Assert.IsNotNull(equipmentClub.Cost.Unit);
        }

        [TestMethod]
        public async Task Client_Should_Return_Equipment_Padded_Armor()
        {
            var equipmentPadded = (Armor)await e5Client.GetEquipmentDetailsAsync("padded");
            Assert.IsNotNull(equipmentPadded);
            Assert.IsNotNull(equipmentPadded.Index);
            Assert.IsNotNull(equipmentPadded.Name);
            Assert.IsNotNull(equipmentPadded.Weight);
            Assert.IsNotNull(equipmentPadded.Cost.Quantity);
            Assert.IsNotNull(equipmentPadded.Cost.Unit);
        }

        [TestMethod]
        public async Task Client_Should_Return_Equipment_AdventuringGear()
        {
            var adventuringGear = (AdventuringGear)await e5Client.GetEquipmentDetailsAsync("abacus");
            Assert.IsNotNull(adventuringGear);
            Assert.IsNotNull(adventuringGear.Index);
            Assert.IsNotNull(adventuringGear.Name);
            Assert.IsNotNull(adventuringGear.Weight);
            Assert.IsNotNull(adventuringGear.Cost.Quantity);
            Assert.IsNotNull(adventuringGear.Cost.Unit);
        }

        [TestMethod]
        public async Task Client_Should_Return_Equipment_EquipmentPack()
        {
            var equipmentPack = (EquipmentPack)await e5Client.GetEquipmentDetailsAsync("burglars-pack");
            Assert.IsNotNull(equipmentPack);
            Assert.IsNotNull(equipmentPack.Index);
            Assert.IsNotNull(equipmentPack.Name);
            Assert.IsNotNull(equipmentPack.Cost.Quantity);
            Assert.IsNotNull(equipmentPack.Cost.Unit);
            Assert.IsTrue(equipmentPack.Contents.Length > 0);
            Assert.IsNotNull(equipmentPack.Contents[0].Item);
            Assert.IsNotNull(equipmentPack.Contents[0].Quantity > 0);
        }

        [TestMethod]
        public async Task Client_Should_Return_MagicItems()
        {
            var magicItemList = await e5Client.GetMagicItemsAsync();
            Assert.IsNotNull(magicItemList);
            Assert.IsTrue(magicItemList.Count > 0);
            Assert.IsTrue(magicItemList.Results.Length > 0);
            Assert.IsNotNull(magicItemList.Results[0].Index);
            Assert.IsNotNull(magicItemList.Results[0].Name);
        }

        [TestMethod]
        public async Task Client_Should_Return_MagicItem_AdamantineArmor()
        {
            var adamantArmor = await e5Client.GetMagicItemDetailsAsync("adamantine-armor");
            Assert.IsNotNull(adamantArmor);
            Assert.IsNotNull(adamantArmor.Index);
            Assert.IsNotNull(adamantArmor.Name);
            Assert.IsNotNull(adamantArmor.DescriptionLines);
            Assert.IsTrue(adamantArmor.DescriptionLines.Length > 0);
        }
    }
}
