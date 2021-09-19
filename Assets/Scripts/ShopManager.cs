using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace KiksAR.Scripts
{
    public class ShopManager : MonoBehaviour
    {
        public static ShopManager Instance;
        
        [SerializeField] private Transform _productParent; //Product Item Container
        [SerializeField] string[] _rendomBrands; //List of Brands
        [SerializeField] Button _btnFilter;
        [SerializeField] GameObject _FilterPanel; //Filter Panel

        private ProductItem[] _productItems; //List of Product Item available in the container

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            //Fetch  all the products
            _productItems = _productParent.GetComponentsInChildren<ProductItem>();

            //Generate Rendom Items
            ResetFilter();
            
            //Show Product panel
            _productParent.gameObject.SetActive(true);

            //set filter button action
            _btnFilter.onClick.AddListener(() => { _FilterPanel.SetActive(true); });
        }

        //Call this method from the filter apply button
        public void ApplyFilter(GENDER gender,CATEGORIES category,List<string> selectedBrands)
        {          
            //get max count of the brand list if selected brands are zero then use rendom brand list
            int _MaxBrands =(selectedBrands.Count==0)? _rendomBrands.Length : selectedBrands.Count;
            
            // Reset all the product which are in the container
            foreach (var item in _productItems)
            {
                //Get Brand Name randomly from the list
                string _brandName = (selectedBrands.Count != 0)? selectedBrands[Random.Range(0, _MaxBrands)] : _rendomBrands[Random.Range(0, _MaxBrands)];
                item.SetBrand(_brandName);
                item.SetSubTitle(string.Format("{0} {1}", gender.ToString(), category.ToString()));
                item.SetPrice(string.Format("${0}.00", Random.Range(30, 651)));
            }
        }

        //Call this method on the reset filter button
        public void ResetFilter()
        {
            int _MaxBrands = _rendomBrands.Length;
            foreach (var item in _productItems)
            {
                string _brandName = _rendomBrands[Random.Range(0, _MaxBrands)];
                item.SetBrand(_brandName);
                int rndValue = Random.Range(0, 4);
                item.SetSubTitle(string.Format("{0} {1}", ((rndValue==0) ?GENDER.Man.ToString():((rndValue==3)?GENDER.Woman.ToString():GENDER.Children.ToString())), ((rndValue == 0) ? CATEGORIES.Cloths.ToString() : ((rndValue == 3) ? CATEGORIES.Jwellery.ToString() : CATEGORIES.Watches.ToString()))));
                item.SetPrice(string.Format("${0}.00", Random.Range(30, 651)));
            }
        }
    }
}

