namespace Hidistro.UI.Common.Validator
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Reflection;

    public class ClientValidatorCollection : ICollection, IEnumerable
    {
        private ValidateTarget owner;
        private ArrayList validators;

        public ClientValidatorCollection(ValidateTarget owner, ArrayList validators)
        {
            this.owner = owner;
            this.validators = validators;
        }

        public void Add(ClientValidator validator)
        {
            this.AddAt(-1, validator);
        }

        public void AddAt(int index, ClientValidator validator)
        {
            if (validator == null)
            {
                throw new ArgumentNullException("validator");
            }
            if (index == -1)
            {
                this.validators.Add(validator);
            }
            else
            {
                this.validators.Insert(index, validator);
            }
            validator.SetOwner(this.owner);
        }

        public void CopyTo(Array array, int index)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            IEnumerator enumerator = this.GetEnumerator();
            while (enumerator.MoveNext())
            {
                array.SetValue(enumerator.Current, index++);
            }
        }

        public IEnumerator GetEnumerator()
        {
            return this.validators.GetEnumerator();
        }

        public int IndexOf(ClientValidator validator)
        {
            if (validator != null)
            {
                return this.validators.IndexOf(validator);
            }
            return -1;
        }

        public void Remove(ClientValidator validator)
        {
            int index = this.IndexOf(validator);
            if (index >= 0)
            {
                this.RemoveAt(index);
            }
        }

        public void RemoveAt(int index)
        {
            if ((index < 0) || (index >= this.Count))
            {
                throw new ArgumentOutOfRangeException("index");
            }
            this.validators.RemoveAt(index);
        }

        [Browsable(false)]
        public int Count
        {
            get
            {
                return this.validators.Count;
            }
        }

        [Browsable(false)]
        public bool IsSynchronized
        {
            get
            {
                return false;
            }
        }

        [Browsable(false)]
        public ClientValidator this[int index]
        {
            get
            {
                return (ClientValidator) this.validators[index];
            }
        }

        [Browsable(false)]
        public object SyncRoot
        {
            get
            {
                return this;
            }
        }
    }
}

