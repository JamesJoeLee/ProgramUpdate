
import requests

response = requests.get("https://api.github.com/repos/stomakun/NeteaseReverseLadder/releases/latest")
print(response.json()["tag_name"])
print(response.json()["zipball_url"])
