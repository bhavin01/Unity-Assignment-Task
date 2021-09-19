using UnityEngine;
using UnityEngine.UI;

namespace KiksAR.Scripts
{
    public class ProductItem : MonoBehaviour
    {
        [SerializeField] private Text _txtBrand,_txtSubTitle,_txtPrice;

        //Set Brand Name
        public void SetBrand(string value)
        {
            _txtBrand.text = value;
        }

        //Set Price
        public void SetPrice(string value)
        {
            _txtPrice.text = value;
        }

        //Set Sub title
        public void SetSubTitle(string value)
        {
            _txtSubTitle.text = value;
        }
    }


    public enum CATEGORIES
    {
        NONE,
        Watches,
        Cloths,
        Jwellery
    }

    public enum GENDER
    {
        NONE,
        Man,
        Woman,
        Children
    }
}