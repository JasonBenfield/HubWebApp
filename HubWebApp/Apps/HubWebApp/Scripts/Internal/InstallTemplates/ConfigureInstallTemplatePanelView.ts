import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { CardAlertView, CardView } from "@jasonbenfield/sharedwebapp/Views/Card";
import { ButtonCommandView } from "@jasonbenfield/sharedwebapp/Views/Command";
import { GridView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { HubTheme } from "../HubTheme";
import { TextHeading3View } from "@jasonbenfield/sharedwebapp/Views/TextHeadings";
import { ToolbarView } from "@jasonbenfield/sharedwebapp/Views/ToolbarView";
import { MarginCss } from "@jasonbenfield/sharedwebapp/MarginCss";
import { FormView } from "@jasonbenfield/sharedwebapp/Views/FormView";
import { FormGroupInputView, FormGroupTextView } from "@jasonbenfield/sharedwebapp/Views/FormGroup";

export class ConfigureInstallTemplatePanelView extends GridView {
    readonly cardAlertView: CardAlertView;
    private readonly formView: FormView;
    readonly templateNameFormGroupView: FormGroupTextView;
    readonly templateNameInputFormGroupView: FormGroupInputView;
    readonly machineNameInputFormGroupView: FormGroupInputView;
    readonly domainInputFormGroupView: FormGroupInputView;
    readonly siteNameInputFormGroupView: FormGroupInputView;
    readonly cancelButton: ButtonCommandView;
    readonly saveButton: ButtonCommandView;

    constructor(container: BasicComponentView) {
        super(container);
        this.styleAsLayout();
        this.height100();
        this.setTemplateRows(CssLengthUnit.flex(1), CssLengthUnit.auto());
        const mainContent = HubTheme.instance.mainContent(this.addCell());
        const cardView = mainContent.addView(CardView);
        cardView.setMargin(MarginCss.bottom(3));
        const titleTextView = cardView.addCardHeader().addView(TextHeading3View);
        titleTextView.addCssName("card-title");
        titleTextView.setText("Configuration Install Template");
        const cardBodyView = cardView.addCardBody();
        this.formView = cardBodyView.addView(FormView);
        this.formView.addOffscreenSubmit();
        const formGroupContainerView = this.formView.addFormGroupContainer();
        this.templateNameFormGroupView = formGroupContainerView.addFormGroupTextView();
        this.templateNameInputFormGroupView = formGroupContainerView.addFormGroupInputView();
        this.machineNameInputFormGroupView = formGroupContainerView.addFormGroupInputView();
        this.domainInputFormGroupView = formGroupContainerView.addFormGroupInputView();
        this.siteNameInputFormGroupView = formGroupContainerView.addFormGroupInputView();
        const toolbar = HubTheme.instance.commandToolbar.toolbar(
            this.addCell().addView(ToolbarView)
        );
        this.cancelButton = HubTheme.instance.commandToolbar.cancelButton(
            toolbar.addButtonCommandToEnd()
        );
        this.cancelButton.setMargin(MarginCss.end(1));
        this.saveButton = HubTheme.instance.commandToolbar.saveButton(
            toolbar.addButtonCommandToEnd()
        );
    }

    handleFormSubmit(action: () => void) {
        this.formView
            .onSubmit()
            .preventDefault()
            .execute(action)
            .subscribe();
    }
}