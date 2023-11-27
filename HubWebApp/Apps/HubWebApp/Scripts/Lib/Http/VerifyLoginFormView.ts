// Generated code
import { BaseFormView } from '@jasonbenfield/sharedwebapp/Views/BaseFormView';
import * as views from '@jasonbenfield/sharedwebapp/Views/FormGroup';
import { IFormGroupLayout } from '@jasonbenfield/sharedwebapp/Views/Types';
import { BasicComponentView } from '@jasonbenfield/sharedwebapp/Views/BasicComponentView';
import { InputView } from '@jasonbenfield/sharedwebapp/Views/InputView';

export interface IVerifyLoginFormView {
	UserName: views.SimpleFieldFormGroupInputView;
	Password: views.SimpleFieldFormGroupInputView;
}

export class DefaultVerifyLoginFormViewLayout implements IFormGroupLayout<IVerifyLoginFormView> {
	addFormGroups(form: VerifyLoginFormView) {
		return {
			UserName: form.addInputFormGroup(),
			Password: form.addInputFormGroup()
		}
	}
}

export class VerifyLoginFormView extends BaseFormView {
	private formGroups: IVerifyLoginFormView;
	
	constructor(container: BasicComponentView) {
		super(container);
	}
	
	addContent(layout?: IFormGroupLayout<IVerifyLoginFormView>) {
		if (!layout) {
			layout = new DefaultVerifyLoginFormViewLayout();
		}
		this.formGroups = layout.addFormGroups(this);
	}
	
	get UserName() { return this.formGroups.UserName; }
	get Password() { return this.formGroups.Password; }
}