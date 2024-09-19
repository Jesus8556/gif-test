from flask import Flask, render_template, request
import requests

app = Flask(__name__)

@app.route('/', methods=['GET', 'POST'])
def index():
    gif_url = None
    if request.method == 'POST':
        number = request.form.get('number')
        if number and number.isdigit():
            number = int(number)
            if 1 <= number <= 20:
                response = requests.get(f'http://gifnet:5145/api/Giphy/{number}')
                if response.status_code == 200:
                    data = response.json()
                    gif_url = data.get('url')
    return render_template('index.html', gif_url=gif_url)

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=3000, debug=True)
