/** @type {import('tailwindcss').Config} */
export default {
    content: [
      "./index.html",
      "./src/**/*.{vue,js,ts,jsx,tsx}",
    ],
    theme: {
      extend: {
        colors:{
    //       <Color x:Key="PageBackground">#FFFBFE</Color>
    // <Color x:Key="ComponentBackground">#38AD49</Color>
    // <Color x:Key="ActiveBackground">#123616</Color>
    // <Color x:Key="SwitchOn">#D9D9D9</Color>
    // <Color x:Key="ActiveText">#FFFBFE</Color>
    // <Color x:Key="LinkColor">#38AD49</Color>
    // <Color x:Key="TextColor">#3F3F3F</Color>
    // <Color x:Key="FormTextColor">#3F3F3F</Color>
    // <Color x:Key="FormBackground">#D9D9D9</Color>
    // <Color x:Key="ErrorText">#B50B0B</Color>
    // <Color x:Key="AcceptText">#38AD49</Color>
    // <Color x:Key="ButtonBackground">#FFFBFE</Color>
    // <Color x:Key="ButtonBorder">#B50B0B</Color>
          page:{
            dark: '#1A1A1A',
            light: '#FFFBFE'
          },
          component:{
            dark: '#123616',
            light: '#38AD49'
          },
          activeBackground:{
            dark: '#1A1A1A',
            light: '#123616'
          },
          activeText:{
            dark: '#FFFBFE',
            light: '#FFFBFE'
          },
          linkColor:{
            dark: '#38AD49',
            light: '#38AD49'
          },
          textColor:{
            dark: '#FFFBFE',
            light: '#3F3F3F'
          },
          formTextColor:{
            dark: '#1A1A1A',
            light: '#3F3F3F'
          },
          formBackground:{
            dark: '#999999',
            light: '#D9D9D9'
          },
          buttonBackground:{
            dark: '#1A1A1A',
            light: '#FFFBFE'
          },
          buttonBorder:{
            dark: '#FC3535',
            light: '#B50B0B'
          }
        }
      },
    },
    plugins: [
        import('@tailwindcss/aspect-ratio'),
        import('@tailwindcss/container-queries'),
        import('@tailwindcss/forms'),
        import('@tailwindcss/typography')
    ],
  }