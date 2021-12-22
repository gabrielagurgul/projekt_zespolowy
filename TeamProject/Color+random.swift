//
//  RandomColor.swift
//  TeamProject
//
//  Created by Grzegorz Gumieniak on 22/12/2021.
//

import Foundation
import SwiftUI

extension Color {
	static var random: Color {
		return Color(
			red: .random(in: 0...1),
			green: .random(in: 0...1),
			blue: .random(in: 0...1)
		)
	}
}
