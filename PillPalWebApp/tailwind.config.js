/** @type {import('tailwindcss').Config} */
export default {
    content: [
      "./index.html",
      "./src/**/*.{vue,js,ts,jsx,tsx}",
    ],
    theme: {
      extend: {
        colors:{
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
          },
          navbar: {
            dark: '#3F3F3F',
            light: '#1A1A1A'
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