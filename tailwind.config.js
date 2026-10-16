/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./Components/**/*.{razor,html,cshtml}",
    "./Pages/**/*.{razor,html,cshtml}",
    "./wwwroot/**/*.html"
  ],
  theme: {
    extend: {
      colors: {
        ivory: '#F7F5F1',
        card: '#FFFEFA',
        plum: {
          deep: '#211A2B',
          secondary: '#30223B',
          surface: '#2a1e36',
          border: '#3d2c4b'
        },
        purple: {
          muted: '#76618E',
          light: '#F3EDF6',
          soft: '#EDE5F2'
        },
        gold: {
          accent: '#E7C46B',
          hover: '#d8b251',
          light: '#FBF5E6',
          border: '#E2BD5F'
        },
        ink: {
          primary: '#211C29',
          muted: '#797381',
          light: '#9E97A6'
        },
        border: {
          subtle: '#E9E4EC',
          strong: '#D5CCD9'
        }
      },
      fontFamily: {
        display: ['Playfair Display', 'Georgia', 'serif'],
        sans: ['DM Sans', 'system-ui', '-apple-system', 'Segoe UI', 'Roboto', 'sans-serif']
      },
      boxShadow: {
        'card': '0 4px 16px -2px rgba(33, 26, 43, 0.06), 0 2px 6px -1px rgba(33, 26, 43, 0.04)',
        'card-hover': '0 12px 28px -4px rgba(33, 26, 43, 0.12), 0 4px 12px -2px rgba(33, 26, 43, 0.06)',
        'featured': '0 0 35px rgba(231, 196, 107, 0.25), 0 16px 36px -6px rgba(18, 12, 24, 0.6)'
      },
      animation: {
        'foil-sweep': 'foil 4s ease-in-out infinite',
        'float-gentle': 'float 6s ease-in-out infinite'
      },
      keyframes: {
        foil: {
          '0%, 100%': { opacity: '0.3', transform: 'rotate(25deg) translateY(-20%)' },
          '50%': { opacity: '0.6', transform: 'rotate(25deg) translateY(20%)' }
        },
        float: {
          '0%, 100%': { transform: 'translateY(0px)' },
          '50%': { transform: 'translateY(-6px)' }
        }
      }
    },
  },
  plugins: [],
}
