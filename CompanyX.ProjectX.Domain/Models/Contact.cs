namespace CompanyX.ProjectX.Domain.Models
{
    /// <summary>
    /// A contact.
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// Contact first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Contact last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Contact company name.
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Contact address line 1.
        /// </summary>
        public string AddressLine1 { get; set; }

        /// <summary>
        /// Contact address line 2.
        /// </summary>
        public string AddressLine2 { get; set; }

        /// <summary>
        /// Contact postcode.
        /// </summary>
        public string Postcode { get; set; }

        /// <summary>
        /// Contact county.
        /// </summary>
        public string County { get; set; }

        /// <summary>
        /// Contact  country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Contact email.
        /// </summary>
        public string Email { get; set; }
    }
}
