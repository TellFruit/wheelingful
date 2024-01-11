FROM node:21.5-alpine

WORKDIR /client

COPY package.json .

RUN npm install

COPY . .

CMD ["npm", "start"]