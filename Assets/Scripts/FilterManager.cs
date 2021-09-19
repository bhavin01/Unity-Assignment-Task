using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace KiksAR.Scripts
{
    public class FilterManager : MonoBehaviour
    {
        [SerializeField] private GameObject _categoryPanel, _genderPanel, _brandPanel; // Panels of the filter screen
        [SerializeField] private ToggleGroup _tCategoryGroup, _tGenderGroup, _tBrandGroup; //Toggle Groups or container of the options button
        [SerializeField] private GENDER _activeGender=GENDER.NONE; // Selected gender
        [SerializeField] private CATEGORIES _activeCategory=CATEGORIES.NONE; //Selected category
        [SerializeField] private Button _btnReset, _btnCancel,_btnApply; // Filter screen buttons

        private List<string> _selectedBrands; // Add selected brands into this list
        private Toggle[] _brandsList; // Add all the available brands  

        private void OnEnable()
        {
            //Reset all the panel on enable this screen
            _tCategoryGroup.SetAllTogglesOff(false);
            _categoryPanel.SetActive(true);
            _genderPanel.SetActive(false);
            _brandPanel.SetActive(false);

            //Reset selection 
            _activeGender = GENDER.NONE;
            _activeCategory = CATEGORIES.NONE;
            _selectedBrands = new List<string>();

            //AddListener of the all the buttons
            _btnApply.onClick.RemoveAllListeners();
            _btnApply.onClick.AddListener(OnApply);

            _btnReset.onClick.RemoveAllListeners();
            _btnReset.onClick.AddListener(OnReset);

            _btnCancel.onClick.RemoveAllListeners();
            _btnCancel.onClick.AddListener(()=> {                    
                OnReset(); // Reset Filter state
                gameObject.SetActive(false); //Hide Filter screen
            });

            //Fetch all the available brands
            _brandsList = _tBrandGroup.GetComponentsInChildren<Toggle>();

        }

        //Call this method when you select any category
        public void OnCategoryChange()
        {
            _genderPanel.SetActive(true);
            _activeCategory = GetSelectedCategory();
        }

        //Call this method when you select any Gender
        public void OnGenderChange()
        {
            _brandPanel.SetActive(true);
            _activeGender = GetSelectedGender();
            foreach (var item in _brandsList)    
                item.isOn = false;
            
            _brandPanel.GetComponent<SetSubCategories>().SetSubCategory(_activeCategory, _activeGender);
        }

        //Call this method when you click on Reset Button
        private void OnReset()
        {
            _tCategoryGroup.SetAllTogglesOff(false);
            _tGenderGroup.SetAllTogglesOff(false);
            foreach (var item in _brandsList)
            {
                item.isOn = false;
            }
            _activeGender = GENDER.NONE;
            _activeCategory = CATEGORIES.NONE;

            //Reset all the product items according to selection
            ShopManager.Instance.ResetFilter();
        }

        //Call this method when you click on Apply Button
        private void OnApply()
        {        
            if (_brandPanel.activeInHierarchy)
            {
                //if the brand panel open then fetch all selected brands 
                GetSelectedBrands();
            }

            //Reset all the product items according to selection
            ShopManager.Instance.ApplyFilter(_activeGender, _activeCategory, _selectedBrands);

            gameObject.SetActive(false);
        }

        //Fetch selected gender
        private GENDER GetSelectedGender()
        {
            string gender = _tGenderGroup.ActiveToggles().FirstOrDefault().GetComponentInChildren<Text>().text;
            if (gender == GENDER.Man.ToString())
            {
                return GENDER.Man;
            }
            else if (gender == GENDER.Woman.ToString())
            {
                return GENDER.Woman;
            }
            else if(gender == GENDER.Children.ToString())
            {
                return GENDER.Children;
            }
            else
            {
                return GENDER.NONE;
            }
        }

        //Fetch selected category
        private CATEGORIES GetSelectedCategory()
        {
            string category = _tCategoryGroup.ActiveToggles().FirstOrDefault().GetComponentInChildren<Text>().text;
            if (category == CATEGORIES.Watches.ToString())
            {
                return CATEGORIES.Watches;
            }
            else if (category == CATEGORIES.Cloths.ToString())
            {
                return CATEGORIES.Cloths;
            }
            else if(category== CATEGORIES.Jwellery.ToString())
            {
                return CATEGORIES.Jwellery;
            }
            else
            {
                return CATEGORIES.NONE;
            }
        }

        //Fetch selected Brands
        private void GetSelectedBrands()
        {
            _selectedBrands = new List<string>();
            foreach (var item in _brandsList)
            {
                if (item.isOn)
                {
                    _selectedBrands.Add(item.GetComponentInChildren<Text>().text);
                }
            }
        }
    }
}
