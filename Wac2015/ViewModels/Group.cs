using System.Collections.Generic;

namespace Wac2015.ViewModels
{
    public class Group<T> : IEnumerable<T>
    {
        public Group(string name, IEnumerable<T> items)
        {
            this.Title = name;
            this.Items = new List<T>(items);
        }
        public override bool Equals(object obj)
        {
            var that = obj as Group<T>;
            return (that != null) && (this.Title.Equals(that.Title));
        }
        public string Title
        {
            get;
            set;
        }
        public IList<T> Items
        {
            get;
            set;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }

    }
}
