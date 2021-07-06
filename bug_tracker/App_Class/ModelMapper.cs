using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class ModelMapper<TSource, TDestinate>
    where TSource : class
    where TDestinate : class
{
    public TDestinate MapModel(TSource source, TDestinate destinate)
    {
        var sourProperties = source.GetType().GetProperties();
        var destProperties = destinate.GetType().GetProperties();

        foreach (var item in sourProperties)
        {
            var dest = destProperties.FirstOrDefault(a => a.Name == item.Name);
            if (dest != null)
            {
                dest.SetValue(destinate, item.GetValue(source, null));
            }
        }

        return destinate;
    }
}

//假設你有兩個 Model 如下。
//public class Model_A
//{
//    public int ID { get; set; }
//    public string NAME { get; set; }
//}

//public class Model_B
//{
//    public int ID { get; set; }
//    public string NAME { get; set; }
//}

//假設 Model_A 是資料來源，想要把 Model_A 的值塞給 Model_B。
////創建 Model_A 
//Model_A modelA = new Model_A()
//{
//    ID = 1,
//    NAME = "Jeff"
//};

////創建 Model_B
//Model_B modelB = new Model_B();

//// 調用 ModelSwitcher
//ModelMapper<Model_A, Model_B> modelSwitcher = new ModelMapper<Model_A, Model_B>();
//// 完成
//Model_B modelBWithData = ModelMapper.MapModel(modelA, modelB);


