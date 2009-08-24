using System;
using System.Collections.Generic;
using System.Text;

namespace SimplePlainNote
{
    public class Notes
    {
		#region Fields (1) 

        private List<frmNote> noteslst;

		#endregion Fields 

		#region Constructors (1) 

        public Notes()
        {
            noteslst = loadnotes();
        }

		#endregion Constructors 

		#region Properties (2) 

        public List<frmNote> GetNotes
        {
            get
            {
                return this.noteslst;
            }
        }

        public int numnotes
        {
            get
            {
                return this.noteslst.Count;
            }
        }

		#endregion Properties 

		#region Methods (1) 

		// Private Methods (1) 

        private List<frmNote> loadnotes()
        {
            //todo
            return null;
        }

		#endregion Methods 
    }
}
