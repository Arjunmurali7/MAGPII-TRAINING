DateTime dob = new DateTime(2004, 9, 18); // Change this
                                          // Step 1: Get today's date
DateTime today = DateTime.Today;
// Step 2: Calculate age
int age = today.Year - dob.Year;
// Step 3: Adjust if birthday has not occurred yet this year
if (dob.Date > today.AddYears(-age))
{
    age--;
}
// Step 4: Check 21 or not
if (age >= 21)
{
    Console.WriteLine("✅ Person is 21 years old or older.");
}
else
{
    Console.WriteLine("❌ Person is not 21 yet.");
}