using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KiksAR.Scripts
{
    public class SetSubCategories : MonoBehaviour
    {
        //Get all the available items of the brands
        [SerializeField] private Transform[] _subCat;

        //List of the brands for the all the gender and category of items
        [SerializeField] private string[] _watchSubCatMan, _clothSubCatMan, _jewellerySubCatMan;
        [SerializeField] private string[] _watchSubCatWoman, _clothSubCatWoman, _jewellerySubCatWoman;
        [SerializeField] private string[] _watchSubCatKids, _clothSubCatKids, _jewellerySubCatKids;

        //Set Brands based on the categbory and gender selection
        public void SetSubCategory(CATEGORIES category, GENDER gender)
        {
            for (int Index = 0; Index < _subCat.Length; Index++)
            {
                Text txtSubCat = _subCat[Index].GetComponentInChildren<Text>();
                switch (category)
                {
                    case CATEGORIES.Watches:
                        switch (gender)
                        {
                            case GENDER.Man:
                                SetText(txtSubCat,_watchSubCatMan[Index]);
                                break;
                            case GENDER.Woman:
                                SetText(txtSubCat, _watchSubCatWoman[Index]);
                                break;
                            case GENDER.Children:
                                SetText(txtSubCat, _watchSubCatKids[Index]);
                                break;
                        }
                        break;
                    case CATEGORIES.Cloths:
                        switch (gender)
                        {
                            case GENDER.Man:
                                SetText(txtSubCat, _clothSubCatMan[Index]);
                                break;
                            case GENDER.Woman:
                                SetText(txtSubCat, _clothSubCatWoman[Index]);
                                break;
                            case GENDER.Children:
                                SetText(txtSubCat, _clothSubCatKids[Index]);
                                break;
                        }
                        break;
                    case CATEGORIES.Jwellery:
                        switch (gender)
                        {
                            case GENDER.Man:
                                SetText(txtSubCat, _jewellerySubCatMan[Index]);
                                break;
                            case GENDER.Woman:
                                SetText(txtSubCat, _jewellerySubCatWoman[Index]);
                                break;
                            case GENDER.Children:
                                SetText(txtSubCat, _jewellerySubCatKids[Index]);
                                break;
                        }
                        break;
                }
            }
        }

        //Set text in the button 
        private void SetText(Text txtText,string value)
        {
            txtText.text = string.Format("{0}", value);
        }
    }
}



