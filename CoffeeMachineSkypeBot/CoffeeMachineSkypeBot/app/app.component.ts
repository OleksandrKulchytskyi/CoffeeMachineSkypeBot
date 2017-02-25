import { Component } from "@angular/core";

@Component(
	{
		selector: 'my-app',
		template: `<h1>Welcone {{name}}!</h1>
					<label>Введите имя:</label>
					<input [(ngModel)]="name" placeholder="name">`
	}
)

export class AppComponent {
	name = '';
}
