
namespace Cifra.FileSystem.FileEntity.Csv
{
    /// <summary>
    /// The record of Magister that contains information of the class and the student
    /// </summary>
    internal sealed class MagisterRecord
    {
        /// <summary>
        /// The id of the student
        /// </summary>
        public int Stamnummer { get; set; }

        /// <summary>
        /// The class name
        /// </summary>
        public string Klas { get; set; }

        /// <summary>
        /// The calling name of the student
        /// </summary>
        public string Roepnaam { get; set; }

        /// <summary>
        /// The infix of the student
        /// </summary>
        public string Tussenvoegsel { get; set; }

        /// <summary>
        /// The surname of the student
        /// </summary>
        public string Achternaam { get; set; }

        /// <summary>
        /// The field of study of the student
        /// </summary>
        public string Studie { get; set; }

        /// <summary>
        /// The email of the student
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The phonenumber of the student
        /// </summary>
        public int Telefoonnummer { get; set; }
    }
}
