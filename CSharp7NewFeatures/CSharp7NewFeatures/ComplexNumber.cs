namespace CSharp7NewFeatures
{
    class ComplexNumber
    {
        float real;
        float imaginary;

        public ComplexNumber(float x , float y)
        {
            this.real = x;
            this.imaginary = y;
        }

        public System.Tuple<float, float> Deconstruction( )
        {
            return new System.Tuple<float, float>( this.real,this.imaginary);            
        }

        public void Deconstruct(out float real, out float imaginary)
        {
            real = this.real;
            imaginary = this.imaginary;
        }
    }
}
