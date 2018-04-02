using DataAccess.Entities;
using System.Linq;

namespace Business.Views
{
    public class ArticleView : BaseView
    {
        public string Name { get; set; }
        public string Abstract { get; set; }

        public static Article ToEntity(ArticleView item)
        {
            if (item == null)
                return null;

            return new Article
            {
                Id = item.Id,
                Name = item.Name,
                Abstract = item.Abstract
            };
        }

        public static ArticleView FromEntity(Article item)
        {
            if (item == null)
                return null;

            return new ArticleView
            {
                Id = item.Id,
                Name = item.Name,
                Abstract = item.Abstract
            };
        }

        public static ArticleView[] FromEntities(Article[] items)
        {
            if (items == null || items.Length == 0)
                return new ArticleView[0];

            return items.Select(FromEntity).ToArray();
        }

        public static Article[] ToEntities(ArticleView[] items)
        {
            if (items == null || items.Length == 0)
                return new Article[0];

            return items.Select(ToEntity).ToArray();
        }
    }
}
