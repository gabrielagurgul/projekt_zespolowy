//
//  CategoryView.swift
//  TeamProject
//
//  Created by Grzegorz Gumieniak on 05/12/2021.
//

import SwiftUI

struct CategoryView: View {
	@Environment(\.colorScheme) var colorScheme
	private let image: Image
	private let name: String
	var body: some View {
		VStack {
			Text(name)
				.font(.largeTitle)
			image
				.resizable()
				.scaledToFit()
			
		}
		.frame(width: 160, height: 160)
		.background(.regularMaterial)
		.mask(RoundedRectangle(cornerRadius: 8))
		.shadow(radius: 16)
	}
}

extension CategoryView {
	init(budgetType: BudgetType) {
		image = Image(budgetType.type.lowercased())
		name = budgetType.type
	}
}

struct CategoryView_Previews: PreviewProvider {
	static var previews: some View {
		CategoryView(budgetType: BudgetType.budgetTypeMock)
	}
}
