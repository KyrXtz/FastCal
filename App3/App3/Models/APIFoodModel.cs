using System;
using System.Collections.Generic;
using System.Text;

namespace App3.Models
{
    public class APIPostFoodModel
    {
        public int fdcId { get; set; }
        public string description { get; set; }
        public string additionalDescriptions { get; set; }
        public string dataType { get; set; }
        public string brandOwner { get; set; }

        public string foodCode { get; set; }
        public string publishedDate { get; set; }
        public string allHighlightFields { get; set; }
        public string score { get; set; }





    }

    public class APIGetFoodModel
    {
        public string type { get; set; }
        public string id { get; set; }
        public string amount { get; set; }
        public nutrient nutrient { get; set; }
        public string gramWeight { get; set; }
    }

    public class APIgramWeightModel
    {
        public string gramWeight { get; set; }
        public string portionDescription { get; set; }

    }

    public class nutrient
    {
        public string id { get; set; }
        public string number { get; set; }
        public string name { get; set; }
        public string rank { get; set; }
        public string unitName { get; set; }

    }

    //public class APIHitsAndPages
    //{
    //    public foodSearchCriteria foodSearchCriteria { get; set; }
    //    public string totalHits { get; set; }
    //    public string totalPages { get; set; }
    //    public string currentPage { get; set; }



    //}
    //public class foodSearchCriteria
    //{
    //    public string generalSearchInput { get; set; }
    //    public string pageNumber { get; set; }
    //    public string requireAllWords { get; set; }

    //}
    public class LocalFoodModel
    {
        public string Name { get; set; }
        public string Cal { get; set; }
        public string Carb { get; set; }
        public string Fat { get; set; }
        public string Prot { get; set; }
        public string MoreInfo { get; set; }
        public IEnumerable<APIgramWeightModel> gramsEnum { get; set; }

    }
}


//"fdcId": 337043,
//    "description": "Veal cutlet or steak, fried, lean only eaten",
//    "additionalDescriptions": "breaded or floured",
//    "dataType": "Survey (FNDDS)",
//    "foodCode": "23205030",
//    "publishedDate": "2019-04-01",
//    "allHighlightFields": "",
//    "score": 212.50763