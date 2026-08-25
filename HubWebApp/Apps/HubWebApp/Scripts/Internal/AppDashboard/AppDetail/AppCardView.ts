import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { BasicTextComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicTextComponentView";
import { CardAlertView, CardView } from "@jasonbenfield/sharedwebapp/Views/Card";
import { FormGroupTextView } from "@jasonbenfield/sharedwebapp/Views/FormGroup";
import { FormGroupContainerView } from "@jasonbenfield/sharedwebapp/Views/FormGroupContainerView";
import { TextHeading3View } from "@jasonbenfield/sharedwebapp/Views/TextHeadings";

export class AppCardView extends CardView {
    readonly alert: CardAlertView;
    readonly appKeyTextView: BasicTextComponentView;
    readonly versionKeyFormGroupView: FormGroupTextView;
    readonly versionNumberFormGroupView: FormGroupTextView;

    constructor(container: BasicComponentView) {
        super(container);
        this.appKeyTextView = this.addCardHeader().addView(TextHeading3View);
        this.appKeyTextView.addCssName("card-title");
        this.alert = this.addCardAlert();
        const cardBodyView = this.addCardBody();
        const formGroupContainerView = cardBodyView.addView(FormGroupContainerView);
        this.versionKeyFormGroupView = formGroupContainerView.addFormGroupTextView();
        this.versionNumberFormGroupView = formGroupContainerView.addFormGroupTextView();
    }
}