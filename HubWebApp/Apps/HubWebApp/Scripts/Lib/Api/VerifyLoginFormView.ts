// Generated code
import { BaseFormView } from '@jasonbenfield/sharedwebapp/Views/BaseFormView';
import { SimpleFieldFormGroupInputView, SimpleFieldFormGroupSelectView } from '@jasonbenfield/sharedwebapp/Views/FormGroup';
import { IFormGroupLayout } from '@jasonbenfield/sharedwebapp/Views/Types';
import { BasicComponentView } from '@jasonbenfield/sharedwebapp/Views/BasicComponentView';

export interface IVerifyLoginFormView {
	UserName: SimpleFieldFormGroupInputView;
	Password: SimpleFieldFormGroupInputView;
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