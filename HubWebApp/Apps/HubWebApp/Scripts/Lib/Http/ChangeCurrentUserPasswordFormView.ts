// Generated code
import { BaseFormView } from '@jasonbenfield/sharedwebapp/Views/BaseFormView';
import * as views from '@jasonbenfield/sharedwebapp/Views/FormGroup';
import { IFormGroupLayout } from '@jasonbenfield/sharedwebapp/Views/Types';
import { BasicComponentView } from '@jasonbenfield/sharedwebapp/Views/BasicComponentView';
import { InputView } from '@jasonbenfield/sharedwebapp/Views/InputView';

export interface IChangeCurrentUserPasswordFormView {
	Password: views.SimpleFieldFormGroupInputView;
	Confirm: views.SimpleFieldFormGroupInputView;
}

export class DefaultChangeCurrentUserPasswordFormViewLayout implements IFormGroupLayout<IChangeCurrentUserPasswordFormView> {
	addFormGroups(form: ChangeCurrentUserPasswordFormView) {
		return {
			Password: form.addInputFormGroup(),
			Confirm: form.addInputFormGroup()
		}
	}
}

export class ChangeCurrentUserPasswordFormView extends BaseFormView {
	private formGroups: IChangeCurrentUserPasswordFormView;
	
	constructor(container: BasicComponentView) {
		super(container);
	}
	
	addContent(layout?: IFormGroupLayout<IChangeCurrentUserPasswordFormView>) {
		if (!layout) {
			layout = new DefaultChangeCurrentUserPasswordFormViewLayout();
		}
		this.formGroups = layout.addFormGroups(this);
	}
	
	get Password() { return this.formGroups.Password; }
	get Confirm() { return this.formGroups.Confirm; }
}